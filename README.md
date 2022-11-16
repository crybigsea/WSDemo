# WSDemo
A websocket server-side test tool, developed using Maui

### 发布
工程目录执行
```shell
New-SelfSignedCertificate -Type Custom `
                          -Subject "CN=MSDemo" `
                          -KeyUsage DigitalSignature `
                          -FriendlyName "MSDemo dev cert" `
                          -CertStoreLocation "Cert:\CurrentUser\My" `
                          -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")
```
复制`Thumbprint`下面的指纹码
`A37A988790C52B2D975EDB826D82DBD39EB85425`

编辑项目文件，添加下面的节点
```xml
    <PropertyGroup Condition="$(TargetFramework.Contains('-windows')) and '$(Configuration)' == 'Release'">
        <GenerateAppxPackageOnBuild>true</GenerateAppxPackageOnBuild>
        <AppxPackageSigningEnabled>true</AppxPackageSigningEnabled>
        <PackageCertificateThumbprint>`指纹码`</PackageCertificateThumbprint> 
    </PropertyGroup>
```

发布
```shell
dotnet publish -f net6.0-windows10.0.19041.0 -c Release
```