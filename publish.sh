#!/bin/bash

dotnet publish Webion.Versioning.Tool \
  -r osx.12-arm64 \
  -c Release \
  /p:PublishSingleFile=true \
  --no-self-contained \
  -o dist/wv

mv dist/wv/Webion.Versioning.Tool dist/wv/wv