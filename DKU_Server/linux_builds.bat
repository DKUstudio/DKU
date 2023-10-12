cd DKU_LoginQueue
call linux.bat
cd bin/Release/net7.0/linux-x64/publish
git add .
git commit -m "auto"
git push
cd ../../../../../

cd ..\DKU_Server
call linux.bat
cd bin/Release/net7.0/linux-x64/publish
git add .
git commit -m "auto"
git push