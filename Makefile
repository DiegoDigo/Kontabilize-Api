db:
	docker-compose up -d

run:
	dotnet run --project=Kontabilize.Api

migrate:
	dotnet ef migrations add ${version} --project=Kontabilize.Infra

update:
	dotnet ef database update --project=Kontabilize.Infra