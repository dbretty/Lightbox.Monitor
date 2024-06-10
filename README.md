[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# Lightbox Monitor

Windows application to automate the Lightbox based on camera usage.

## Introduction

This application can be run as a standalone application or wrapped in a service and will monitor for your webcam being in use. When in use it will send an Api POST to a given Uri to control smart lighting from an ESP 32 board, when you stop using your camera it will resend a POST turning the lights off.

## Usage

To run the application perform the following

```monitor.exe http://uri_for_lights_on http://uri_for_lights_off```

The application will validate the number of parameters passed in as well as valid Uri's being passed.

For full information and documentation please visit [https://bretty.me.uk/projects/](https://bretty.me.uk/projects/).
