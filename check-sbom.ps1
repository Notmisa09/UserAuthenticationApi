$ErrorActionPreference = "Stop"

$SbomPath   = ".\sbom\bom.json"
$Prohibidas = @("AGPL-3.0", "GPL-3.0", "SSPL", "BUSL")

function Fallar($m) {
    Write-Host ""
    Write-Host "ERROR: $m" -ForegroundColor Red
    exit 1
}

# --- Detectar solucion ---
$sln = Get-ChildItem -Filter *.sln | Select-Object -First 1
if (-not $sln) { Fallar "No encontre .sln en $(Get-Location)" }
$proyecto = $sln.Name

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host " Gate de SBOM  |  $proyecto"                        -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan

# --- 1. GENERAR ---
Write-Host "`n==> [1/4] Generando SBOM" -ForegroundColor Yellow

dotnet restore $proyecto
if ($LASTEXITCODE -ne 0) { Fallar "Fallo dotnet restore" }

dotnet CycloneDX $proyecto -o ./sbom --json --exclude-dev
if ($LASTEXITCODE -ne 0) { Fallar "Fallo la generacion del SBOM (revisa que CycloneDX este instalado)" }

if (-not (Test-Path $SbomPath)) { Fallar "No se genero $SbomPath" }

# --- 2. VALIDAR CONTENIDO ---
Write-Host "`n==> [2/4] Validando contenido" -ForegroundColor Yellow

$sbom = Get-Content $SbomPath -Raw | ConvertFrom-Json
$comps = @($sbom.components)

Write-Host "    Formato: $($sbom.bomFormat) $($sbom.specVersion)"
Write-Host "    Componentes: $($comps.Count)"
if ($comps.Count -lt 1) { Fallar "SBOM vacio" }

$sinVersion = @($comps | Where-Object { [string]::IsNullOrWhiteSpace($_.version) })
if ($sinVersion.Count -gt 0) {
    $sinVersion | ForEach-Object { Write-Host "      - $($_.name)" -ForegroundColor Red }
    Fallar "Componentes sin version"
}

$sinPurl = @($comps | Where-Object { -not $_.purl }).Count
Write-Host "    Sin PURL: $sinPurl"
Write-Host "    OK" -ForegroundColor Green

# --- 3. LICENCIAS ---
Write-Host "`n==> [3/4] Licencias" -ForegroundColor Yellow

$filas = foreach ($c in $comps) {
    $lic = "SIN-LICENCIA"
    if ($c.PSObject.Properties.Name -contains 'licenses') {
        $arr = @($c.licenses)
        if ($arr.Count -gt 0 -and $arr[0].license) {
            if ($arr[0].license.id)        { $lic = $arr[0].license.id }
            elseif ($arr[0].license.name)  { $lic = $arr[0].license.name }
        }
    }
    [PSCustomObject]@{ Licencia = $lic; Paquete = "$($c.name)@$($c.version)" }
}

Write-Host "    Distribucion:"
$filas | Group-Object Licencia | Sort-Object Count -Descending | ForEach-Object {
    Write-Host ("      {0,4}  {1}" -f $_.Count, $_.Name)
}

$malas = @($filas | Where-Object {
    $l = $_.Licencia
    @($Prohibidas | Where-Object { $l -like "*$_*" }).Count -gt 0
})

if ($malas.Count -gt 0) {
    Write-Host ""
    $malas | ForEach-Object { Write-Host "      - $($_.Paquete) -> $($_.Licencia)" -ForegroundColor Red }
    Fallar "Licencias prohibidas encontradas"
}
Write-Host "    OK: ninguna prohibida" -ForegroundColor Green

# --- 4. VULNERABILIDADES ---
Write-Host "`n==> [4/4] Vulnerabilidades" -ForegroundColor Yellow

$salida = (dotnet list $proyecto package --vulnerable --include-transitive 2>&1 | Out-String)
Write-Host $salida

$hits = ([regex]::Matches($salida, '(?im)\b(High|Critical)\b')).Count
if ($hits -gt 0) { Fallar "$hits hallazgos High/Critical" }

Write-Host "    OK: sin High/Critical" -ForegroundColor Green

Write-Host "`n==================================================" -ForegroundColor Green
Write-Host " SBOM VALIDO - despliegue autorizado"                -ForegroundColor Green
Write-Host "==================================================" -ForegroundColor Green
exit 0