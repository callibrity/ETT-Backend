#!/bin/bash
#rm ~/.aspnet/https/*
dotnet dev-certs https --clean
dotnet dev-certs https --trust -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p password