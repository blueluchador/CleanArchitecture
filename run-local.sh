#!/bin/bash

# Optional params:
# - (-t | --tests) Run unit and integration tests
# - (-d | --debug) Launch in debug mode

shopt -s failglob
set -eu -o pipefail

while test $# -gt 0; do
  case "$1" in
    -t|--test) run_tests=true; shift ;;
    -d|--debug) launch_debug=true; shift ;;
    *) break ;;
  esac
done

project='run-local'

host_url='http://localhost:5999'
launch_url=("$host_url"/api-docs)

# The docker-compose files are applied in this order. Putting the 'override' file
# at the end ensures that the service will be built from the Dockerfile if necessary
base_file='./docker-compose.yml'
override_file='./docker-compose.override.yml'
test_file='./docker-compose.tests.yml'

if [ "${launch_debug-}" = true ]; then
  override_file='./docker-compose.debug.yml'
fi

if [ "${run_tests-}" = true ]; then
  compose_file_args=('-f' "$test_file")
else
  compose_file_args=('-f' "$base_file" '-f' "$override_file")
fi

# cleanup
Cleanup() {
  echo
  docker-compose -p $project "${compose_file_args[@]}" down -v --remove-orphans
  popd
  exit 0
}

trap Cleanup EXIT

# Make sure PWD is same directory as this script (root of solution)
pushd "${BASH_SOURCE%/*}"

# docker run and publish to container
docker-compose -p $project "${compose_file_args[@]}" up -d --build --always-recreate-deps

if [ "${run_tests-}" = true ]; then
  docker-compose -p $project "${compose_file_args[@]}" logs -f clean-architecture.tests
else
  if [ "${launch_debug-}" = true ]; then
    echo 'You can now start your debugging session in VS Code.'
  fi
  
  echo -n 'Launching the Clean Architecture service.'
  until curl -s -o /dev/null $host_url/ping
  do
    echo -n '.'
    sleep 0.5
  done
  
  platform=$(uname)
  if [[ 'Darwin' == "${platform}" ]]; then # MacOS
    open "${launch_url-}"
  elif [[ 'Linux' == "${platform}" ]] && [[ -n "$(command -v xdg-open)" ]]; then # Linux with xdg-open
    xdg-open "${launch_url-}"
  elif grep -q icrosoft /proc/version; then # WSL
    cmd.exe /c start "${launch_url-}"
  elif [[ "$platform" == *"MINGW64_NT-10.0"* ]]; then # Windows
    cmd /c start "http://localhost:5999/api-docs"
  fi
  
  echo
  echo 'The Clean Architecture container is running on host '$host_url'.'
  
  docker-compose -p $project "${compose_file_args[@]}" logs -f
fi