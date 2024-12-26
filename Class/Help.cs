using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjSuperClean.Utils.Utils;

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
            Console.WriteLine("guiá-lo através das opções e menus disponíveis no ");
            Console.WriteLine("sistema. Leia com atenção e siga as orientações para");
            Console.WriteLine("utiliza-lo de forma eficiente.");
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<      SOBRE UTILIZADORES                    >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1. **Cadastro de Utilizador**:");
            Console.WriteLine("   - Acesse o menu **'Cadastro de Utilizador'**.");
            Console.WriteLine("   - Deve ter no máximo 8 caracteres e não deve ser vazio.");
            Console.WriteLine("   - Exemplo: 'joao123'.");
            Console.WriteLine("   - Caso o nome seja novo, o sistema cria um outro utilizador.");
            Console.WriteLine();

            Console.WriteLine("2. **Alteração de Nome de Utilizador**:");
            Console.WriteLine("   - Acesse o menu **'Alterar Nome de Utilizador'**.");
            Console.WriteLine("   - Digite o novo nome, respeitando os critérios mencionados.");
            Console.WriteLine("   - O sistema valida o nome antes de realizar a alteração.");
            Console.WriteLine();

            Console.WriteLine("3. **Exclusão de Utilizador**:");
            Console.WriteLine("   - Acesse o menu **'Excluir Utilizador'**.");
            Console.WriteLine("   - Informe o nome do utilizador que deseja excluir.");
            Console.WriteLine("   - Atenção: a exclusão é permanente e não pode ser desfeita.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<      SOBRE RESIDÊNCIAS                     >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("4. **Cadastro de Residência**:");
            Console.WriteLine("   - Após criar o utilizador, informe o nome da sua residência.");
            Console.WriteLine("   - A residência será associada automaticamente ao utilizador.");
            Console.WriteLine();

            Console.WriteLine("5. **Alteração de Nome da Residência**:");
            Console.WriteLine("   - Acesse o menu **'Alterar Nome da Residência'**.");
            Console.WriteLine("   - Digite o novo nome.");
            Console.WriteLine("   - Não pode ser vazio e deve respeitar o limite de caracteres.");
            Console.WriteLine();

            Console.WriteLine("6. **Exclusão de Residência**:");
            Console.WriteLine("   - Acesse o menu **'Excluir Residência'**.");
            Console.WriteLine("   - Atenção: Todos os pisos e áreas associados serão excluídos.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<      SOBRE PISOS                           >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("7. **Cadastro de Piso**:");
            Console.WriteLine("   - Acesse o menu **'Adicionar Piso'**.");
            Console.WriteLine("   - Informe um número único para o piso (ex.: '01').");
            Console.WriteLine();

            Console.WriteLine("8. **Alteração de Piso**:");
            Console.WriteLine("   - Acesse o menu **'Alterar Piso'**.");
            Console.WriteLine("   - Informe o número do piso a ser alterado.");
            Console.WriteLine("   - Deve ser existente.");
            Console.WriteLine();
            Console.WriteLine("9. **Exclusão de Piso**:");
            Console.WriteLine("   - Acesse o menu **'Excluir Piso'**.");
            Console.WriteLine("   - Informe o número do piso que deseja excluir.");
            Console.WriteLine("   - Atenção: a exclusão é permanente.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<      SOBRE ÁREAS                           >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("10. **Cadastro de Área**:");
            Console.WriteLine("    - Acesse o menu **'Adicionar Área'**.");
            Console.WriteLine("    - Informe o nome da área (ex.: 'Sala', 'Cozinha').");
            Console.WriteLine();

            Console.WriteLine("11. **Alteração de Área**:");
            Console.WriteLine("    - Acesse o menu **'Alterar Área'**.");
            Console.WriteLine("    - Informe o nome da área a ser alterada.");
            Console.WriteLine("    - Utilize nomes distintos para áreas semelhantes");
            Console.WriteLine("    - (ex.: 'Quarto 1', 'Quarto 2').");
            Console.WriteLine( );
            Console.WriteLine("12. **Exclusão de Área**:");
            Console.WriteLine("    - Acesse o menu **'Excluir Área'**.");
            Console.WriteLine("    - Informe o nome da área que deseja excluir.");
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("<<<      MENSAGENS DE ERRO COMUNS              >>>");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("- 'Nome inválido': Nome vazio ou acima do limite.");
            Console.WriteLine("- 'Residência inexistente': Informe uma residência válida.");
            Console.WriteLine("- 'Piso não encontrado': Número do piso não cadastrado.");
            Console.WriteLine("- 'Área não encontrada': Nome da área inexistente.");
            Console.WriteLine();

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal...");
            Console.ReadKey();
            Console.Clear();
            HeaderProgramUserStart();

        }
    }
}
