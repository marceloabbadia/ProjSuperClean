using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjSuperClean.Class
{
    public class Help
    {
        public static void ExibirManualDoUtilizador()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("<<<      MANUAL DO UTILIZADOR - SUPERCLEAN     >>>");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("Bem-vindo ao SUPERCLEAN! Este manual detalhado irá");
            Console.WriteLine("guiá-lo através das opções e menus disponíveis no");
            Console.WriteLine("sistema. Leia com atenção e siga as orientações para");
            Console.WriteLine("utilizá-lo de forma eficiente.");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<      REGISTRAR LIMPEZA DA ÁREA               >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Em todos os menus será possível registrar a limpeza");
            Console.WriteLine("da área. Ao acessar essa opção, será apresentada a");
            Console.WriteLine("relação das áreas, ordenadas de forma decrescente por");
            Console.WriteLine("vencimento. Ou seja, as que têm o vencimento mais");
            Console.WriteLine("próximo, ou que já estão vencidas, serão listadas");
            Console.WriteLine("primeiro. Basta informar o código da área e a limpeza");
            Console.WriteLine("será atualizada com a data do dia automaticamente,");
            Console.WriteLine("e a data da próxima limpeza será recalculada de");
            Console.WriteLine("acordo com o intervalo de limpeza cadastrado.");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<   REGISTRAR EXCLUSÃO DA LIMPEZA DA ÁREA       >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Em todos os menus será possível registrar a exclusão");
            Console.WriteLine("da última data de limpeza, sendo restabelecida a data");
            Console.WriteLine("anterior, de acordo com o intervalo de limpeza");
            Console.WriteLine("cadastrado.");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<     CONSULTA DE LIMPEZA - SIMULADOR DE DATAS  >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Este módulo permite ao utilizador consultar todas as");
            Console.WriteLine("suas áreas cadastradas em formato árvore, segmentando");
            Console.WriteLine("andares (pisos) e suas divisões (áreas). Ao pressionar");
            Console.WriteLine("qualquer tecla direcional (setas) do teclado, será");
            Console.WriteLine("possível navegar em um simulador de datas progressivo ou");
            Console.WriteLine("regressivo, atualizando automaticamente a simulação");
            Console.WriteLine("com barras verticais indicativas de limpeza. Onde:");
            Console.WriteLine("Barras verdes: indicam que a divisão está limpa.");
            Console.WriteLine("Barras amarelas: indicam que a divisão está entre 1");
            Console.WriteLine("dia antes da data prevista de limpeza e 2 dias após, se");
            Console.WriteLine("não foi limpa.");
            Console.WriteLine("Barras vermelhas: indicam que a divisão está com a");
            Console.WriteLine("limpeza vencida há mais de 2 dias e limitadas para 20 dias.");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<             SOBRE UTILIZADORES               >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1. **Cadastro de Utilizador**:");
            Console.WriteLine("   - Acesse o menu **'Cadastro de Utilizador'**.");
            Console.WriteLine("   - O nome deve ter no máximo 8 caracteres, não pode");
            Console.WriteLine("     ser vazio e deve conter apenas letras e números.");
            Console.WriteLine("   - Caracteres especiais como @, #, &, *, etc., não são");
            Console.WriteLine("     permitidos.");
            Console.WriteLine("   - Exemplo válido: 'joao123'.");
            Console.WriteLine("   - Exemplo inválido: 'joao@123'.");
            Console.WriteLine("   - Caso o nome seja novo, o sistema criará um utilizador");
            Console.WriteLine("     automaticamente.");
            Console.WriteLine();

            Console.WriteLine("2. **Alteração de Nome de Utilizador**:");
            Console.WriteLine("   - Acesse o menu **'Alterar Nome de Utilizador'**.");
            Console.WriteLine("   - Digite o novo nome, respeitando os critérios");
            Console.WriteLine("     mencionados.");
            Console.WriteLine("   - O sistema valida o nome antes de realizar a alteração.");
            Console.WriteLine();

            Console.WriteLine("3. **Exclusão de Utilizador**:");
            Console.WriteLine("   - Acesse o menu **'Excluir Utilizador'**.");
            Console.WriteLine("   - Informe o nome do utilizador que deseja excluir.");
            Console.WriteLine("   - Atenção: a exclusão é permanente e não pode ser");
            Console.WriteLine("     desfeita.");
            Console.WriteLine("   - Após a exclusão, será automaticamente redirecionado ao");
            Console.WriteLine("     início do programa.");
            Console.WriteLine();


            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<             SOBRE RESIDÊNCIAS               >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("4. **Cadastro de Residência**:");
            Console.WriteLine("   - Após criar o utilizador, informe o nome da sua");
            Console.WriteLine("     residência.");
            Console.WriteLine("   - A residência será associada automaticamente ao");
            Console.WriteLine("     utilizador.");
            Console.WriteLine("   - O utilizador so podera ter uma residencia associada    " );

            Console.WriteLine("5. **Alteração de Nome da Residência**:");
            Console.WriteLine("   - Acesse o menu **'Alterar Nome da Residência'**.");
            Console.WriteLine("   - Digite o novo nome.");
            Console.WriteLine("   - Não pode ser vazio e deve respeitar o limite de");
            Console.WriteLine("     caracteres.");
            Console.WriteLine();

            Console.WriteLine("6. **Exclusão de Residência**:");
            Console.WriteLine("   - Acesse o menu **'Excluir Residência'**.");
            Console.WriteLine("   - Atenção: todos os andares(pisos) e áreas associados");
            Console.WriteLine("     serão excluídos.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<               SOBRE ANDARES(PISOS)           >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("7. **Cadastro de Andar(Piso)**:");
            Console.WriteLine("   - Acesse o menu **'Adicionar Andar(Piso)'**.");
            Console.WriteLine("   - Informe um número único para o andar(piso) (ex.:");
            Console.WriteLine("     '01').");
            Console.WriteLine();

            Console.WriteLine("8. **Alteração de Andar(Piso)**:");
            Console.WriteLine("   - Acesse o menu **'Alterar Andar(Piso)'**.");
            Console.WriteLine("   - Informe o número do andar(piso) a ser alterado.");
            Console.WriteLine("   - O número deve existir.");
            Console.WriteLine();

            Console.WriteLine("9. **Exclusão de Andar(Piso)**:");
            Console.WriteLine("   - Acesse o menu **'Excluir Andar(Piso)'**.");
            Console.WriteLine("   - Informe o número do andar(piso) que deseja excluir.");
            Console.WriteLine("   - Atenção: a exclusão é permanente.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<               SOBRE ÁREAS (DIVISÕES)         >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("10. **Cadastro de Área (Divisão)**:");
            Console.WriteLine("    - Acesse o menu **'Adicionar Área (Divisão)'**.");
            Console.WriteLine("    - Informe o nome da área (ex.: 'Sala', 'Cozinha').");
            Console.WriteLine();

            Console.WriteLine("11. **Alteração de Área (Divisão)**:");
            Console.WriteLine("    - Acesse o menu **'Alterar Área (Divisão)'**.");
            Console.WriteLine("    - Informe o nome da área (divisão) a ser alterada.");
            Console.WriteLine("    - Utilize nomes distintos para áreas semelhantes.");
            Console.WriteLine("    - Exemplo: 'Quarto 1', 'Quarto 2'.");
            Console.WriteLine();

            Console.WriteLine("12. **Exclusão de Área (Divisão)**:");
            Console.WriteLine("    - Acesse o menu **'Excluir Área (Divisão)'**.");
            Console.WriteLine("    - Informe o nome da área (divisão) que deseja excluir.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<        MENSAGENS DE ERRO COMUNS              >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("- 'Nome inválido': Nome vazio ou acima do limite.");
            Console.WriteLine("- 'Residência inexistente': Informe uma residência");
            Console.WriteLine("  válida.");
            Console.WriteLine("- 'Andar(Piso) não encontrado': Número do andar(piso)");
            Console.WriteLine("  não cadastrado.");
            Console.WriteLine("- 'Área(Divisão) não encontrada': Nome da área");
            Console.WriteLine("  inexistente.");
            Console.WriteLine();

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal...");
            Console.ReadKey();
            Console.Clear();
            Program.HeaderProgramUserStart();
        }

    }
}
