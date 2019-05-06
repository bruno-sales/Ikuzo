# Ikuzo

A Ikuzo é uma API desenvolvida em .net C# para consulta de informações de linhas, ônibus e monitoramento de GPS. Os dados são atualizados a cada minuto.

### Instalação

Para executar a Ikuzo API localmente, você irá precisar:

  - Visual Studio 2017 v15.3.+ .Net Framework 4.6.+
  - SQL Server 2014+

### Rest API
As respostas serão sempre me formato JSON e As URLs disponíveis para consulta de cada item são:

*Todas as datas/horas das consultas são em formato UTC (+3 horas em relação ao horário brasileiro).*

#### Ônibus
 Retorna uma lista com informações básicas de todos os ônibus cadastrados.
```sh
[GET] /v1/api/buses/
```
- - -
Retorna informações detalhadas de um ônibus específico dado identificador.
```sh
[GET] /v1/api/buses/{busId}
```
| Campo | Tipo | Descrição |
| ------ | ------ | ------ |
| busId | string | Identificador único do ônibus

Exemplo: /v1/api/buses/C51555
- - - 
Retorna lista com informações de GPS de ônibus próximos à uma geolocalização.
```sh
[GET] /v1/api/buses/nearby?lat={latitude}&lon={longitude}&precision={precision}&line={line}
```
| Campo | Tipo | Descrição |
| ------ | ------ | ------ |
| latitude | float | Campo obrigatório com a informação da latitude a ser pesquisada. Precisão de 6 casas decimais.
| longitude | float | Campo obrigatório com a informação da longitude a ser pesquisada. Precisão de 6 casas decimais.
| precision | int | Opcional. Campo que pode variar de 0 a 100, referente ao alcance da posição em relação à geolocalização passada. Caso 100, será retornado a lista com ônibus que estejam a até aproximadamente 1 KM do ponto pesquisado. Caso 0, serão retornado ônibus até a 3 KM do ponto pesquisado. Por padrão este campo é 100. 
| line | string | Opcional. Identificador da linha de ônibus. Caso esse parâmetro seja preenchido, o atributo {precision} é descartado, e será retornado uma lista com todos os ônibus da linha e suas posições em relação à geolocalização passada. 

Exemplo: /v1/api/buses/nearby?lat=-22.914456&lon=43.226733
- - - 

#### Linhas
 Retorna uma lista com informações básicas de todas as linhas de ônibus cadastradas.
```sh
[GET] /v1/api/lines/
```
- - -
Retorna informações detalhadas de uma linha específica dado identificador.
```sh
[GET] /v1/api/lines/{lineId}
```
| Campo | Tipo | Descrição |
| ------ | ------ | ------ |
| lineId | string | Identificador único da linha de ônibus

Exemplo: /v1/api/lines/107
- - - 
Retorna lista de linhas de ônibus tenham seu itinerário que passam próximas à uma geolocalização.
```sh
[GET] /v1/api/buses/nearby?lat={latitude}&lon={longitude}&precision={precision}
```
| Campo | Tipo | Descrição |
| ------ | ------ | ------ |
| latitude | float | Campo obrigatório com a informação da latitude a ser pesquisada. Precisão de 6 casas decimais.
| longitude | float | Campo obrigatório com a informação da longitude a ser pesquisada. Precisão de 6 casas decimais.
| precision | int | Opcional. Campo que pode variar de 0 a 100, referente ao alcance da posição em relação à geolocalização passada. Caso 100, será retornado a lista com linhas com itinerários próximos até 45 metros do ponto pesquisado. Caso 0, serão retornado ônibus até a 200 metros do ponto pesquisado. Por padrão este campo é 100. 

Exemplo: /v1/api/lines/local?lat=-22.914456&lon=43.226733&precision=50
- - - 
### Contribuição, dúvidas e feedbacks
Entre em contato [comigo](https://github.com/bruno-sales) (:
  
### Outras informações importantes
Os dados da Ikuzo API são sincronizados com a [RioBus](https://github.com/RioBus), que por sua vez são sincronizados com a Prefeitura do Rio de Janeiro e a FETRANSPOR.
