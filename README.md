Testando no Swagger
Acesse o Swagger (/swagger).

Clique em POST /api/Empacotamento.

Clique em Try it out.

Cole o exemplo de JSON abaixo:

[
  {
    "pedidoId": 1,
    "produtos": [
{
"produtoId": "GAMEBOY",
"dimensao": {
"id": 1,
"altura": 30,
"largura": 40,
"comprimento": 80
}
},
{
"produtoId": "NOTEBOOK",
"dimensao": {
"id": 2,
"altura": 50,
"largura": 60,
"comprimento": 40
}
},
{
"produtoId": "PS5",
"dimensao": {
"id": 3,
"altura": 70,
"largura": 50,
"comprimento": 40
}
}
]
  }
]
