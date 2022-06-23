# ID_PrimaveraV10_Code

## Repositório de Bibliotecas de Classes escritas para aumentar a funcionalidade e workflow do Primavera ERP v10.0.

### Criado para responder a necessidades específicas de vários clientes de modo a melhorar a qualidade e eficiência do trabalho feito em Primavera.
#### Sendo uma aplicação corporativa de grandes dimensões, o Primavera trabalha com várias dependências/referências e consegue ser confuso para quem não está ainda habituado ao back-end ou não conhece as suas complexas interligações; assim, os ficheiros podem parecer sobre-comentados, numa tentativa de esclarecer o seu funcionamento e evitar confusão.

### ---  LISTA DE SOLUÇÕES --- ###

**ASRLB-ImportacaoFatura** - Cria um dataset a partir de um ficheiro .txt pre-formatado, e compara os dados com os presentes em tabelas da BD Primavera. Se todos os dados forem válidos, o programa cria, valida e submete faturas em lote. O programa em sí funciona como um WinForm, acedido através de uma nova secção criada no friso superior do Primavera chamado FERRAMENTAS. A janela contém as seguintes opções: escolha do ficheiro (por defeito vai buscar a um caminho especifico no servidor da empresa), e outros elementos para editar alguns campos das faturas.

**MDL_Obs** - Copia os valores de uma célula de uma linha de um documento de vendas, para uma caixa de texto no cabeçalho desse mesmo documento se certos critérios forem correspondidos.



## Repository of Class Libraries written to extend the functionality and workflow of Primavera ERP v10.0. 

### Made to fulfill different customers requests to improve and minimize their company's workload. 
#### Being a large corporate application, Primavera works with a lot of dependencies/references; these are added and commented, respectively, at the top. You might also find commented blocks of code. These are test scenarios that require minimal program interaction to work (compared to the final solution) while giving the same result, facilitating testing.<br></br>

### --- SOLUTION LIST --- ###

**ASRLB-ImportacaoFatura** - Creates a dataset from a formatted .txt file and validates the data with that present on specific Primavera DB tables. If all data is valid, the program then creates, validates and submits invoices in bulk. It functions as a WinForm and is accessed via a newly created tab in the main window's ribbon called FERRAMENTAS; the window features a file chooser (with a default path to a server location), and a date and value pickers to customize some data in the invoices.

**MDL_Obs** - Copies the value of a table cell to a textbox in the Vendas/ContasCorrentes forms of Primavera v10 if the document value type meets certain criteria.
