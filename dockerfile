# Используем SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы проекта и восстанавливаем зависимости
COPY ["FinanceManager/FinanceManager.csproj", "FinanceManager/"]
RUN dotnet restore "FinanceManager/FinanceManager.csproj"

# Копируем весь код и собираем приложение
COPY . .
WORKDIR "/src/FinanceManager"
RUN dotnet build "FinanceManager.csproj" -c Release -o /app/build

# Публикуем приложение как самодостаточный файл (self-contained)
FROM build AS publish
RUN dotnet publish "FinanceManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ с runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceManager.dll"]