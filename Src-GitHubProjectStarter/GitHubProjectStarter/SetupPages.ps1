Clear-Host
Write-Output "------------------------------------------------------------------------------------------------"
Write-Output "    ____   ____    ___    ____    ___     __  ______       _____   ___  ______  __ __  ____     "
Write-Output "   |    \ |    \  /   \  |    |  /  _]   /  ]|      |     / ___/  /  _]|      ||  |  ||    \    "
Write-Output "   |  o  )|  D  )|     | |__  | /  [_   /  / |      |    (   \_  /  [_ |      ||  |  ||  o  )   "
Write-Output "   |   _/ |    / |  O  | __|  ||    _] /  /  |_|  |_|     \__  ||    _]|_|  |_||  |  ||   _/    "
Write-Output "   |  |   |    \ |     |/  |  ||   [_ /   \_   |  |       /  \ ||   [_   |  |  |  :  ||  |      "
Write-Output "   |  |   |  .  \|     |\  `  ||     |\     |  |  |       \    ||     |  |  |  |     ||  |      "
Write-Output "   |__|   |__|\_| \___/  \____||_____| \____|  |__|        \___||_____|  |__|   \__,_||__|      "
Write-Output "                                                                                                "
Write-Output "         https://github.com/Hyper-Dragon/TemplatesHD/tree/main/Src-GitHubPagesStarter           "
Write-Output "------------------------------------------------------------------------------------------------"
Write-Output ">> Setting up github pages"

$TITLE = "TODO"
$REPLACE_SITEROOT = "TEMPLATETEST"
$REPLACE_THIS_TITLE = "TO DO"
$REPLACE_SPONSOR="Hyper-Dragon"

$REPLACE_SOURCE="source"

Write-Output "   >> Getting Lorem Ipsum for markdown"
$REPLACE_THIS_ABOUT = (Invoke-Webrequest -UseBasicParsing -Uri http://metaphorpsum.com/paragraphs/2).Content
$REPLACE_THIS_INSTALL = (Invoke-Webrequest -UseBasicParsing -Uri http://metaphorpsum.com/paragraphs/1).Content
$REPLACE_THIS_USAGE = (Invoke-Webrequest -UseBasicParsing -Uri http://metaphorpsum.com/paragraphs/5).Content
$REPLACE_THIS_LICENSE = (Invoke-Webrequest -UseBasicParsing -Uri http://metaphorpsum.com/paragraphs/3).Content

Write-Output "   >> Copying Files"
Copy-Item .\docsTemplate\ -Destination .\copyToRoot\docs\ -Recurse
Copy-Item .\githubTemplate\ -Destination .\copyToRoot\.github\ -Recurse
Copy-Item .\rootTemplate\* -Destination .\copyToRoot\

Write-Output "   >> Updating index.md"
((Get-Content -path ".\copyToRoot\docs\indexTemplate.md" -Raw) -replace 'REPLACE_THIS_INSTALL',$REPLACE_THIS_INSTALL `
                                                    -replace 'REPLACE_THIS_ABOUT',$REPLACE_THIS_ABOUT     `
                                                    -replace 'REPLACE_THIS_USAGE',$REPLACE_THIS_USAGE     `
                                                    -replace 'REPLACE_THIS_LICENSE',$REPLACE_THIS_LICENSE `
                                                    -replace 'REPLACE_THIS_TITLE',$REPLACE_THIS_TITLE )   `
                                                    | Set-Content -Path ".\copyToRoot\docs\index.md"

Write-Output "   >> Updating head-custom.html"
((Get-Content -path ".\copyToRoot\docs\_includes\head-customTemplate.html" -Raw)`
                                                    -replace 'REPLACE_SITEROOT', $REPLACE_SITEROOT )`
                                                    | Set-Content -Path ".\copyToRoot\docs\_includes\head-custom.html"

Write-Output "   >> Updating default.html"  
((Get-Content -path ".\copyToRoot\docs\_layouts\defaultTemplate.html" -Raw)`
                                                    -replace 'REPLACE_SPONSOR', $REPLACE_SPONSOR )`
                                                    | Set-Content -Path ".\copyToRoot\docs\_layouts\default.html"

Write-Output "   >> Removing Pages Templates"
Remove-Item .\copyToRoot\docs\indexTemplate.md                                 
Remove-Item .\copyToRoot\docs\_includes\head-customTemplate.html
Remove-Item .\copyToRoot\docs\_layouts\defaultTemplate.html                        
Move-Item   .\copyToRoot\docs\RENAME.code-workspace  .\copyToRoot\docs\"$TITLE".code-workspace

Write-Output ">> Pages Setup Complete"

((Get-Content -path ".\copyToRoot\.github\dependabotTemplate.yml" -Raw)`
                                                    -replace 'REPLACE_SOURCE', $REPLACE_SOURCE )`
                                                    | Set-Content -Path ".\copyToRoot\.github\dependabot.yml"

Move-Item   .\copyToRoot\RENAME.code-workspace  .\copyToRoot\"$TITLE"Project.code-workspace
Remove-Item ".\copyToRoot\.github\dependabotTemplate.yml"

Write-Output ">> Project Setup Complete"

Write-Output "------------------------------------------------------------------------------------------------"
Write-Output ""