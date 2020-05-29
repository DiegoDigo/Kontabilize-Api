run:
	dotnet run --project=Kontabilize.Api

migrate:
	cd Kontabilize.Infra && dotnet ef migrations add ${version} 

update:
	cd Kontabilize.Infra && dotnet ef database update