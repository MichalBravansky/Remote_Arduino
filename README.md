# Remote Arduino Project for Expedice Mars

## Motivation
Due to the pandemic, the 2020 semifinals of the international competition Expedice Mars took place in an online environment. I wanted to allow the participants to work on a circuit with an Arduino chip remotely. However, I didn’t find any software online which could do that and so I decided to create one myself.

## Introduction

This project consists of an ASP.NET application used to upload the code, display results and access the MS SQL database. The other part is a Python script, which downloads codes from the ASP.NET API, runs it on a local Arduino chip and returns the output.

## Implementation

First, you need to set up a MS SQL database with the create_script from the repository. After adding the connection string into "appsettings.json" file, you can deploy the web application. Then just create a Python file with all the credentials and run the file "compiler.py" on a computer connected to the specific Arduino chip. 
