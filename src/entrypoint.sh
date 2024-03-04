#!/bin/bash 
echo "Waiting 20 seconds to start backend"
sleep 20;
echo "Backend Starting.."
dotnet HoopHub.API.dll
