# db-first Setup and use example

    download and install microsoft package

1. download .Net SDK X64
   https://dotnet.microsoft.com/download
2. install dotnet-ef tools
   dotnet tool install --global detnet-ef 7.0
   dotnet tool install --global detnet-ef 8.0
   // 1.2.1 update dotnet-ef toll
   dotnet tool update --global dotnet-ef
3. add relational package to project
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer

   // 1.4 check application.csproj add under PackageReference
   `<ItemGroup>`
   `<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="7.0.10" />`
   `<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.10" />`
   `<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="7.0.10" />`
   `</ItemGroup>`
4. Verify installation
   // 2.1 安裝相關套件:
   dotnet restore

   // 2.2 檢視 EF 指令:
   dotnet ef

# generate entity from database

### .net 5 ef cmd foodPos

* dotnet ef dbContext scaffold "server=(local)\SqlExpress;database=FoodPos2021;Trusted_Connection=True;" Microsoft.EntityFrameWorkCore.SqlServer -c "AppDbContext" -o FoodPos/Domain -f

### .net 7 must add TrustServerCertificate=true;

* dotnet ef dbContext scaffold "server=(local)\SqlExpress01;database=HrModel;Trusted_Connection=True;TrustServerCertificate=true;Microsoft.EntityFrameWorkCore.SqlServer -c "AppDbContext" -o Hr/Models -f
* dotnet ef dbContext scaffold "server=(local)\SqlExpress01;database=VillageModel;Trusted_Connection=True;TrustServerCertificate=true;" Microsoft.EntityFrameWorkCore.SqlServer -c "AppDbContext" -o Models -n Api.Models -f

### .net 10

* dotnet ef dbContext scaffold "server=(local)\SqlExpress01;database=VillageModel;Trusted_Connection=True;TrustServerCertificate=true;" Microsoft.EntityFrameWorkCore.SqlServer -c "AppDbContext" -o Models -n Api.Models --force --no-pluralize --use-database-names

