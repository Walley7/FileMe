!define PRODUCT_NAME "File Me"

Name "${PRODUCT_NAME}"
OutFile "FileMeSetup.exe"
Icon "filer.ico"
BrandingText " "
SilentInstall silent

Section

  SetOutPath "$TEMP\${PRODUCT_NAME}"

  ; Add all files that your installer needs here
  File "setup.exe"
  File "FileMeSetup.msi"

  ExecWait "$TEMP\${PRODUCT_NAME}\setup.exe"
  RMDir /r /REBOOTOK "$TEMP\${PRODUCT_NAME}"

SectionEnd