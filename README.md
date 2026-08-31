<p align="center">
    <img src="Images/Bloxstrap.png" width="120">
</p>

<h1 align="center">BreakerStrap</h1>

<p align="center">
Um bootstrapper alternativo para o Roblox, focado em desempenho e num visual vermelho próprio.<br>
Baseado no <a href="https://github.com/bloxstraplabs/bloxstrap">Bloxstrap</a>.
</p>

----

## O que é o BreakerStrap?

BreakerStrap é um substituto para o inicializador (bootstrapper) padrão do Roblox no Windows, com recursos extras de personalização e otimização. É um fork do [Bloxstrap](https://github.com/bloxstraplabs/bloxstrap), reconstruído com foco em:

- 🔴 **Identidade visual própria** — tema vermelho em todo o app
- ⚡ **Otimização de verdade** — Fast Flags curadas dentro da allowlist oficial da Roblox, prioridade de processo alta e suporte a GPU dedicada
- 🧹 **Interface mais enxuta** — removemos telas que não fazem sentido pro projeto

## Funcionalidades

- **WhiteList** — aplica de uma vez um conjunto de Fast Flags de otimização gráfica (renderização, grama, LOD de CSG, MSAA), todas dentro da lista oficialmente permitida pela Roblox
- **Modo Performance Máxima** — liga a WhiteList inteira + prioridade alta de processo + preferência de GPU dedicada, tudo com um clique
- **Prioridade alta de processo** — dá mais tempo de CPU pro Roblox no Windows
- **GPU dedicada** — em notebooks com placa integrada + dedicada, força o uso da dedicada
- **Editor de Fast Flags** — interface completa pra quem quiser mexer manualmente em flags específicas
- Telemetria e Discord Rich Presence vêm **desativados por padrão** (você ativa se quiser)

> [!IMPORTANT]
> Desde 30/09/2025, a Roblox aplica uma allowlist do lado do servidor pra Fast Flags de cliente. Apenas flags dessa lista oficial têm efeito real no jogo — qualquer outra é ignorada silenciosamente. A WhiteList do BreakerStrap foi construída em cima dessa lista oficial.

## Instalação

Baixe o instalador mais recente na aba [Releases](../../releases) deste repositório e execute-o. É necessário Windows.

## Compilando você mesmo

Requer .NET 6 SDK e Visual Studio (ou `dotnet build`):

```
git clone https://github.com/Sxntoss/BreakerStrap.git
cd BreakerStrap
dotnet build --configuration Release
```

## Problemas e sugestões

Encontrou um bug ou tem uma ideia? [Abra uma issue](https://github.com/Sxntoss/BreakerStrap/issues/new).

## Créditos

Fork feito por [Sxntoss](https://github.com/Sxntoss).

O BreakerStrap é um fork do [Bloxstrap](https://github.com/bloxstraplabs/bloxstrap), criado originalmente por **pizzaboxer** e mantido pela BloxstrapLabs, distribuído sob [licença MIT](LICENSE). Praticamente toda a base de código, arquitetura e boa parte da interface vêm do projeto original — o BreakerStrap não existiria sem ele.

Este é um projeto não-oficial e não tem qualquer afiliação com a Roblox Corporation ou com a equipe do Bloxstrap.

## Licença

Distribuído sob a [licença MIT](LICENSE), a mesma do projeto original.
