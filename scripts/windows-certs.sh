#!/bin/bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust -ep $USERPROFILE\\.aspnet\\https\\aspnetapp.pfx -p password