param(
    [string]$SourcePng,
    [string]$DestIco
)

Add-Type -AssemblyName System.Drawing

$bmp = [System.Drawing.Bitmap]::FromFile($SourcePng)
try {
    $fs = [System.IO.File]::Open($DestIco, [System.IO.FileMode]::Create)
    try {
        $bw = New-Object System.IO.BinaryWriter($fs)
        # ICONDIR header (reserved=0, type=1, count=1)
        $bw.Write([byte]0)
        $bw.Write([byte]0)
        $bw.Write([byte]1)
        $bw.Write([byte]0)
        $bw.Write([byte]1)
        $bw.Write([byte]0)

        # ICONDIRENTRY
        $width  = [byte]([Math]::Min(255, $bmp.Width))
        $height = [byte]([Math]::Min(255, $bmp.Height))
        if ($width -eq 256) { $width = 0 }
        if ($height -eq 256) { $height = 0 }
        $bw.Write($width)     # width (0 means 256)
        $bw.Write($height)    # height (0 means 256)
        $bw.Write([byte]0)    # color count
        $bw.Write([byte]0)    # reserved
        $bw.Write([UInt16]0)  # planes
        $bw.Write([UInt16]0)  # bitcount

        $ms = New-Object System.IO.MemoryStream
        $bmp.Save($ms, [System.Drawing.Imaging.ImageFormat]::Png)
        $pngBytes = $ms.ToArray()
        $bw.Write([UInt32]$pngBytes.Length) # bytes in resource
        $bw.Write([UInt32]22)               # offset to image data

        # Write PNG image data
        $bw.Write($pngBytes)
        $bw.Flush()
    } finally {
        $fs.Close()
    }
} finally {
    $bmp.Dispose()
}

