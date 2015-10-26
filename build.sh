# probably supposed to use NAnt or something

mcs -t:library -r:./constructorio/Newtonsoft.Json.7.0.1/lib/net40/Newtonsoft.Json.dll ./constructorio/constructorio.cs
# copy it over so tests work
cp ./constructorio/constructorio.dll ./test/
