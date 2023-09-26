#!/bin/bash

echo 'Running tests...'

dotnet test tests/CleanArchitecture.Tests/CleanArchitecture.Tests.csproj \
  -l "console;verbosity=normal" -c Release --no-build \
  /p:CollectCoverage=true \
  /p:CoverletOutputFormat="json%2copencover" \
  /p:CoverletOutput=/coverage/ \
  /p:MergeWith="/coverage/coverage.json"

echo
echo 'Running integration tests...'

dotnet test tests/CleanArchitecture.IntegrationTests/CleanArchitecture.IntegrationTests.csproj \
  -l "console;verbosity=normal" -c Release --no-build \
  /p:CollectCoverage=true \
  /p:CoverletOutputFormat="json%2copencover" \
  /p:CoverletOutput=/coverage/ \
  /p:MergeWith="/coverage/coverage.json"