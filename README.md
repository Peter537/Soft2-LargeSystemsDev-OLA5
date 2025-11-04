# Soft2-LargeSystemsDev-OLA5

1. `docker-compose up -d` i denne folder

2. Kør nogen API calls, ex med Postman:

POST http://localhost:5288/api/order/place

```json
{
    "status": "Done",
    "items": [
        {
            "name": "",
            "price": 100
        }
    ]
}
```

3. tjek Prothemeus Metrics på URL http://localhost:5288/metrics

4. log ind på Grafana http://localhost:3000/

5. gå til http://localhost:3000/connections/datasources, og tryk "Add new data source", vælg "Prometheus", sæt "Connection" til `http://prometheus:9090` (alt er hosted på Docker containers, så ikke localhost) og gem

6. gå til http://localhost:3000/dashboards, tryk "Create Dashboard", tryk "Add visualization", vælg datasource, sæt en metric i "Select metric" (ex. order_placed_total)

Eller, du kan vælge at bruge [monitoring/sla-dashboard.json](./monitoring/sla-dashboard.json) ved at gå i "Create Dashboard", og så trykke "Import a dashboard", og indsætte JSON og load den
