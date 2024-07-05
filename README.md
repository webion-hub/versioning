# Description

wv is an app versioning tool.

# Installation
To install, first download the binaries from the [releases](https://github.com/webion-hub/versioning/releases) section.

## OSX
```shell
sudo ln -s /absolute/path/to/Webion.Versioning.Tool /usr/local/bin/wv
```

# Usage

## Updating a version
```shell
wv incr my_app
```

**Updating project files**
```shell
# Json example
wv incr my_app \
  --file apps/my_app/package.json \  # Path to the project file
  --path '$.version' \  # Path to the json object, using json paths
  --lang json
  
# Xml example
wv incr my_app \
  --file apps/my_app/package.xml \  # Path to the project file
  --path '//version' \  # Path to the xml node, using XML paths
  --lang xml
```

## Listing apps
```shell
wv ls
```

## Tagging the current commit
The tag command can be used to tag a commit with the current app version, and then push it to origin.
```shell
wv tag
```