# Sample app
## What is this app for?
Here is Console app to get block and transaction information from **Etherscan** Provider and store it in MYSQL database.

## Infrastructure
This app was built against following there layers model
- Application layer
- Service layer
- Data access layer ( with help of EF Core)

## Setup
- Create db using docker by running this command
   `   docker run -p 3306:3306 --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mysql:latest
   `
- Create new account on **Etherscan** , grab the api key and replace those key with `[YOUR_API_KEY]` in appsettings.json file
- Then execute the executable file - Sample.exe. That's it.

## Pros
- Apply TDD
- Apply retry-pattern

## What to enhance?
- Should apply some job service to retry the failed block or transaction stuff if there is some transient network issue
- Should apply some notification service to send a notification if there some request failed to **Etherscan**

# Happy coding :)