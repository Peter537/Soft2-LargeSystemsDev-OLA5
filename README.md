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


