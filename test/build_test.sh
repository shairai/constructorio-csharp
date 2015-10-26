# probably supposed to use NAnt or something
# actually, build and run

mcs -t:library -r:../constructorio/constructorio.dll -r:/usr/lib/mono/gac/nunit.framework/2.6.0.0__96d09a1eb7f44a77/nunit.framework.dll constructortests.cs
nunit-console ./constructortests.dll
