# probably supposed to use NAnt or something
# actually, build and run

mcs -t:library -r:./constructorio/Newtonsoft.Json.7.0.1/lib/net40/Newtonsoft.Json.dll ./constructorio/constructorio.cs
# copy it over so tests work
cp ./constructorio/constructorio.dll ./test/

rm ./test/constructortests.dll
mcs -t:library -r:./constructorio/constructorio.dll -r:./test/Newtonsoft.Json.7.0.1/lib/net40/Newtonsoft.Json.dll -r:/usr/lib/mono/gac/nunit.framework/2.6.0.0__96d09a1eb7f44a77/nunit.framework.dll ./test/constructortests.cs

echo "built"

nunit-console ./test/constructortests.dll
