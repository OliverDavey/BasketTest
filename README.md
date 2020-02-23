# BasketTest

Flow For Testing:

1. Create a new basket using the endpoint `POST /Basket`
2. Apply offer with endpoint `PATCH /Basket/{basketId}/Offer`
3. Redeem gift cards with endpoint `PATCH /Basket/{basketId}/GiftCard`
4. Fetch basket info with endpoint `GET /Basket/{basketId}`

## Test Scenarios
### Basket 1
Create Basket with
```
{
  "items": [
    {
      "productId": "shoddy_hat",
      "quantity": 1
    },
    {
      "productId": "warm_jumper",
      "quantity": 1
    }
  ]
}
```

Redeem gift voucher
```
{
  "voucherCode": "XXX-XXX"
}
```

Retrieve Basket
Expected Total: `60.15`

### Basket 2
Create Basket with
```
{
  "items": [
    {
      "productId": "luxury_hat",
      "quantity": 1
    },
    {
      "productId": "thin_jumper",
      "quantity": 1
    }
  ]
}
```

Apply offer
```
{
  "offerCode": "headgear_5_50"
}
```
Response message 
```
"There are no products in your basket applicable to voucher headgear_5_50"
```

Retrieve Basket
Expected Total: `51.00`

### Basket 3
Create Basket with
```
{
  "items": [
    {
      "productId": "luxury_hat",
      "quantity": 1
    },
    {
      "productId": "thin_jumper",
      "quantity": 1
    },
    {
      "productId": "head_light",
      "quantity": 1
    }
  ]
}
```

Apply offer
```
{
  "offerCode": "headgear_5_50"
}
```

Retrieve Basket
Expected Total: `51.00`

### Basket 4
Create Basket with
```
{
  "items": [
    {
      "productId": "luxury_hat",
      "quantity": 1
    },
    {
      "productId": "thin_jumper",
      "quantity": 1
    }
  ]
}
```

Apply offer
```
{
  "offerCode": "anything_5_50"
}
```

Redeem gift voucher
```
{
  "voucherCode": "XXX-XXX"
}
```

Retrieve Basket
Expected Total: `41.00`

### Basket 5
Create Basket with
```
{
  "items": [
    {
      "productId": "luxury_hat",
      "quantity": 1
    },
    {
      "productId": "large_gift",
      "quantity": 1
    }
  ]
}
```

Apply offer
```
{
  "offerCode": "anything_5_50"
}
```
Response message 
```
"You have not reached the spend threshold for voucher anything_5_50. Spend another £25.01 to receive £5.00 discount from your basket total"
```