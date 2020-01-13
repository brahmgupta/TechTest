# Welcome !

Hi, this sample application is developed using .Net Core 3.0 for WebAPis. 

# Projects 

- .Net Core WebAPI - This is located under '**WooliesX/src**' path.

# Get Started

## Web API - .Net Core Application

1. Load API solution `\WooliesX.sln` in Visual Studio 2019
2. Run Application

## Hosted on Azure
[https://cleanarchitectureweb20200112115635.azurewebsites.net/swagger](https://cleanarchitectureweb20200112115635.azurewebsites.net/swagger)
#### Exercise 1 - User End Point
[https://cleanarchitectureweb20200112115635.azurewebsites.net/api/users/user](https://cleanarchitectureweb20200112115635.azurewebsites.net/api/users/user)


#### Exercise 2 - Product Sort End Point
[https://cleanarchitectureweb20200112115635.azurewebsites.net/api/products/sort?sortOption=low](https://cleanarchitectureweb20200112115635.azurewebsites.net/api/products/sort?sortOption=low)

 1. Ascending Sort **Passed**
 2. Descending Sort **Passed**
 3. High Sort **Passed**
 4. Low Sort **Passed**
 5. Recommended Sort **Failed** - 
Though, I'm grouping products from all customers and returning most ordered products in descending order. Little more explanation on Recommended Sort might help !