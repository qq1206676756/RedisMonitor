echo off
echo 清理开始
del /s/q/f *.user
del /s/q/f *.suo

@for /r . %%I in (debug) do if exist "%%I" rd/s/q "%%I"
@for /r . %%I in (bin) do if exist "%%I" rd/s/q "%%I"
@for /r . %%I in (obj) do if exist "%%I" rd/s/q "%%I"
@for /r . %%I in (clientbin) do if exist "%%I" rd/s/q "%%I"

echo 清理完毕
pause