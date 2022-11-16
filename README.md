# WSDemo
A websocket server-side test tool, developed using Maui

### ����
����Ŀ¼ִ��
```shell
New-SelfSignedCertificate -Type Custom `
                          -Subject "CN=MSDemo" `
                          -KeyUsage DigitalSignature `
                          -FriendlyName "MSDemo dev cert" `
                          -CertStoreLocation "Cert:\CurrentUser\My" `
                          -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}")
```
����`Thumbprint`�����ָ����
`A37A988790C52B2D975EDB826D82DBD39EB85425`

�༭��Ŀ�ļ����������Ľڵ�
```xml
    <PropertyGroup Condition="$(TargetFramework.Contains('-windows')) and '$(Configuration)' == 'Release'">
        <GenerateAppxPackageOnBuild>true</GenerateAppxPackageOnBuild>
        <AppxPackageSigningEnabled>true</AppxPackageSigningEnabled>
        <PackageCertificateThumbprint>`ָ����`</PackageCertificateThumbprint> 
    </PropertyGroup>
```

����
```shell
dotnet publish -f net6.0-windows10.0.19041.0 -c Release
```