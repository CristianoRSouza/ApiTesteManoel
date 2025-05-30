# 📦 API de Empacotamento de Produtos

Esta API calcula a melhor alocação de produtos em caixas, com base nas dimensões fornecidas.

---

## 🚀 Como Testar no Swagger

Você pode testar a API diretamente pela interface Swagger.

### ✅ Passos para Testar

1. Acesse o Swagger na URL:


2. Vá até a rota:


3. Clique no botão `Try it out`.

4. Cole o seguinte exemplo de JSON no corpo da requisição:

```json
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
```
5 Clique em Execute.

Observações
A API irá tentar alocar os produtos nas caixas predefinidas.
Se um produto não couber em nenhuma caixa disponível, será lançada uma exceção do tipo:

CaixaForaDoPadraoException


Caso de falha
Exemplo de Produto que Não Cabe

{
  "produtoId": "TV_GIGANTE",
  "dimensao": {
    "id": 99,
    "altura": 100,
    "largura": 100,
    "comprimento": 100
  }
}
