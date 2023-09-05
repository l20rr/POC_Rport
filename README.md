# ReportApp
## Componente de Feedback para Aplicação
Este componente é projetado para permitir que os usuários forneçam feedback sobre a aplicação. Ele permite aos usuários compartilhar sua opinião geral sobre a aplicação, relatar bugs e até mesmo enviar imagens, vídeos ou gravar a tela para uma melhor compreensão dos problemas encontrados.

## Funcionalidades Principais
Opinião Geral: Os usuários podem compartilhar suas opiniões gerais sobre a aplicação. Isso fornece informações valiosas sobre como melhorar a experiência do usuário.

Relatório de Bugs: Os usuários podem relatar bugs encontrados na aplicação. Isso ajuda a identificar e corrigir problemas de maneira eficiente.

Envio de Fotos e Vídeos: Os usuários têm a opção de anexar fotos e vídeos para ilustrar melhor os problemas que encontraram.

Gravação de Tela: Os usuários podem gravar a tela em tempo real para capturar ações específicas que levaram a um problema.

## Requisitos de Configuração
Certifique-se de que a aplicação e a API estão configuradas conforme abaixo antes de usar este componente:

A aplicação está rodando em https://localhost.

A API está rodando em https://localhost:7046/ com endpoints api/[controller].

Uma base de dados local deve ser configurada e conectada à aplicação.

Configuração do Banco de Dados
Configure uma base de dados local.

Conecte a aplicação à base de dados editando a string de conexão no arquivo appsettings.json.

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "NomeDaSuaConexao"
}
No Gestor de Pacotes do Visual Studio, execute o seguinte comando para aplicar as migrações e atualizar o banco de dados:
sql
Copy code
update-database
Como Usar o Componente
Inclua o componente de feedback em sua aplicação onde você deseja que os usuários possam fornecer feedback.
html
Copy code
 <FeedbackMenuComponent/>
Os usuários podem preencher os campos de opinião geral, relatório de bugs e anexar fotos, vídeos ou gravar a tela conforme necessário.

O componente enviará o feedback para a API, onde os dados serão processados e armazenados no banco de dados local.

Desenvolvido por: Lucas Santos e Mariana Alberto.
