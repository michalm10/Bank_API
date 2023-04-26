# Solution to back-end task

Written in .NET

## Running  
Clone repository and navigate to the main folder.

### With .NET installed
This requires .NET SDK 6.0

```
dotnet run --project nbp_api/nbp_api.csproj
```

### Docker
To build type command:
```
docker build -t nbp_api:latest .
```

To run type command:
```
docker run -it --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 nbp_api:latest
```

Now server should be running on `localhost:5000`

You can go to `localhost:5000/swagger/index.html` in the browser to use UI to test API

## Using API Instructions
### API performs 3 operations:
1) Get average exchange rate for specidied currency and date:
    - format: `localhost:5000/exchanges/rate/<currency code>/<date>`
    - example:
    ```
    curl http://localhost:5000/exchanges/rate/chf/2022-12-12
    ```  
    returns `4.7624`
2) Get minimal and maximal exchage rates for specified currency from specified number of last quotations:
    - format: `localhost:5000/exchanges/min-max/<currency code>/<number of last quotations>`
    - example:
    ```
    curl http://localhost:5000/exchanges/min-max/chf/10
    ```  
    returns
    `{"min":{"value":4.6914,"date":"2023-04-20"},"max":{"value":4.7361,"date":"2023-04-13"}}`
3) Get maximal difference between ask and buy rates from specified number of last quotations:
    - format: `localhost:5000/exchanges/diff/<currency code>/<number of last quotations>`
    - example:
    ```
    curl http://localhost:5000/exchanges/diff/chf/10
    ```  
    returns
    `{"value":0.09480000000000022,"date":"2023-04-13"}`  
    
Return values were acquired on 25.04.2023
  
### Project also contains Unit tests in Tests folder
