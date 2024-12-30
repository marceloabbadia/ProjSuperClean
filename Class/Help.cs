using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static ProjSuperClean.Utils.Utils;

namespace ProjSuperClean.Class
{
    public class Help
    {
        private static void Options() 
        {
            Console.WriteLine("1- Incluir e Excluir limpeza");
            Console.WriteLine("2- Consulta das limpezas das Áreas");
            Console.WriteLine("3- Sobre os Utilizadores");
            Console.WriteLine("4- Sobre as Residências");
            Console.WriteLine("5- Sobre os Andares (Pisos)");
            Console.WriteLine("6- Sobre as Áreas (Divisões)");
            Console.WriteLine("7- Mensagens de Erros Comuns");
            Console.WriteLine("8- Sair");
        }

        public static void HelpMenu(Guid userId, string utilizador)
        {
            bool again = true;

            while (again)
            {
                Console.Clear();

                Title("MANUAL DO UTILIZADOR - SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Bem-vindo ao SUPERCLEAN! Este manual detalhado irá");
                Console.WriteLine("guiá-lo através das opções e menus disponíveis no");
                Console.WriteLine("sistema. Leia com atenção e siga as orientações para");
                Console.WriteLine("utilizá-lo de forma eficiente.");
                Console.WriteLine();


                Options();

                string opcaoInput = Console.ReadLine();

                try
                {

                    if (int.TryParse(opcaoInput.Trim(), out int about))
                    {
                        switch (about)
                        {
                            case 1:
                                Console.Clear();

                                Title("REGISTRAR LIMPEZA DA ÁREA");
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
                                Title("REGISTRAR EXCLUSÃO DA LIMPEZA DA ÁREA");
                                Console.WriteLine();
                                Console.WriteLine("Em todos os menus será possível registrar a exclusão");
                                Console.WriteLine("da última data de limpeza, sendo restabelecida a data");
                                Console.WriteLine("anterior, de acordo com o intervalo de limpeza");
                                Console.WriteLine("cadastrado.");
                                Console.WriteLine();
                                break;

                            case 2:
                                Console.Clear();

                                Title("CONSULTA DE LIMPEZA - SIMULADOR DE DATAS");
                                Console.WriteLine();
                                Console.WriteLine("Este módulo permite ao utilizador consultar todas as");
                                Console.WriteLine("suas áreas cadastradas em formato árvore, segmentando");
                                Console.WriteLine("andares (pisos) e suas divisões (áreas). Ao pressionar");
                                Console.WriteLine("qualquer tecla direcional (setas) do teclado, será");
                                Console.WriteLine("possível navegar em um simulador de datas progressivo");
                                Console.WriteLine("ou regressivo, atualizando automaticamente a simulação");
                                Console.WriteLine("com barras verticais indicativas de limpeza.");
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("||| BARRAS VERDES");
                                Console.ResetColor();
                                Console.WriteLine("- Indicam que a divisão está limpa.");
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("||| BARRAS AMARELAS");
                                Console.ResetColor();
                                Console.WriteLine("- Indicam que a divisão está entre 1 dia antes da data");
                                Console.WriteLine("prevista de limpeza e 2 dias após se não foi limpa. ");
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("||| BARRAS VERMELHAS");
                                Console.ResetColor();
                                Console.WriteLine("- Indicam que a divisão está com a limpeza vencida há ");
                                Console.WriteLine("mais de 2 dias.");

                                Console.WriteLine("As barras são exibidas até o limite de 20 dias.");

                                break;

                            case 3:
                                Console.Clear();

                                Title("SOBRE UTILIZADORES");
                                Console.WriteLine();
                                Console.WriteLine(" **Cadastro de Utilizador**:");
                                Console.WriteLine("   - Acesse o menu **'Cadastro de Utilizador'**.");
                                Console.WriteLine("   - O nome deve ter no máximo 8 caracteres, não pode");
                                Console.WriteLine("     ser vazio e deve conter apenas letras e números.");
                                Console.WriteLine("   - Caracteres especiais como @, #, &, *, etc., não são");
                                Console.WriteLine("     permitidos.");
                                Console.WriteLine("   - Correto: 'joao123'  X  Incorreto: 'joao@123'. ");
                                Console.WriteLine("   - Caso o nome seja novo, o sistema criará um utilizador");
                                Console.WriteLine("     automaticamente.");
                                Console.WriteLine();
                                Console.WriteLine(" **Alteração de Nome de Utilizador**:");
                                Console.WriteLine("   - Acesse o menu **'Alterar Nome de Utilizador'**.");
                                Console.WriteLine("   - Digite o novo nome, respeitando os critérios");
                                Console.WriteLine("     mencionados.");
                                Console.WriteLine("   - O sistema valida o nome antes de realizar a alteração.");
                                Console.WriteLine();
                                Console.WriteLine(" **Exclusão de Utilizador**:");
                                Console.WriteLine("   - Acesse o menu **'Excluir Utilizador'**.");
                                Console.WriteLine("   - Informe o nome do utilizador que deseja excluir.");
                                Console.WriteLine("   - Atenção: A exclusão é permanente.");
                                Console.WriteLine("   - Após a exclusão, será automaticamente redirecionado ao");
                                Console.WriteLine("     início do programa.");
                                break;

                            case 4:
                                Console.Clear();

                                Title("SOBRE RESIDÊNCIAS");
                                Console.WriteLine();
                                Console.WriteLine(" **Cadastro de Residência**:");
                                Console.WriteLine("   - Após criar o utilizador, informe o nome da sua");
                                Console.WriteLine("     residência.");
                                Console.WriteLine("   - A residência será associada automaticamente ao");
                                Console.WriteLine("     utilizador.");
                                Console.WriteLine("   - O utilizador só poderá ter uma residência associada.");
                                Console.WriteLine();
                                Console.WriteLine(" **Alteração de Nome da Residência**:");
                                Console.WriteLine("   - Acesse o menu **'Alterar Nome da Residência'**.");
                                Console.WriteLine("   - Digite o novo nome.");
                                Console.WriteLine("   - Não pode ser vazio e deve respeitar o limite de");
                                Console.WriteLine("     caracteres.");
                                Console.WriteLine();
                                Console.WriteLine(" **Exclusão de Residência**:");
                                Console.WriteLine("   - Acesse o menu **'Excluir Residência'**.");
                                Console.WriteLine("   - Atenção: todos os andares(pisos) e áreas associados");
                                Console.WriteLine("     serão excluídos.");
                                break;

                            case 5:
                                Console.Clear();

                                Title("SOBRE ANDARES(PISOS)");
                                Console.WriteLine();
                                Console.WriteLine(" **Cadastro de Andar(Piso)**:");
                                Console.WriteLine("   - Acesse o menu **'Adicionar Andar(Piso)'**.");
                                Console.WriteLine("   - Informe um número único para o andar(piso) (ex.:");
                                Console.WriteLine("     '01').");
                                Console.WriteLine();
                                Console.WriteLine(" **Alteração de Andar(Piso)**:");
                                Console.WriteLine("   - Acesse o menu **'Alterar Andar(Piso)'**.");
                                Console.WriteLine("   - Informe o número do andar(piso) a ser alterado.");
                                Console.WriteLine("   - O número deve existir.");
                                Console.WriteLine();
                                Console.WriteLine(" **Exclusão de Andar(Piso)**:");
                                Console.WriteLine("   - Acesse o menu **'Excluir Andar(Piso)'**.");
                                Console.WriteLine("   - Informe o número do andar(piso) que deseja excluir.");
                                Console.WriteLine("   - Atenção: a exclusão é permanente.");
                                break;

                            case 6:
                                Console.Clear();

                                Title("SOBRE ÁREAS (DIVISÕES)");
                                Console.WriteLine(" **Cadastro de Área (Divisão)**:");
                                Console.WriteLine("    - Acesse o menu **'Adicionar Área (Divisão)'**.");
                                Console.WriteLine("    - Informe o nome da área (ex.: 'Sala', 'Cozinha').");
                                Console.WriteLine();
                                Console.WriteLine(" **Alteração de Área (Divisão)**:");
                                Console.WriteLine("    - Acesse o menu **'Alterar Área (Divisão)'**.");
                                Console.WriteLine("    - Informe o nome da área (divisão) a ser alterada.");
                                Console.WriteLine("    - Utilize nomes distintos para áreas semelhantes.");
                                Console.WriteLine("    - Exemplo: 'Quarto 1', 'Quarto 2'.");
                                Console.WriteLine();
                                Console.WriteLine(" **Exclusão de Área (Divisão)**:");
                                Console.WriteLine("    - Acesse o menu **'Excluir Área (Divisão)'**.");
                                Console.WriteLine("    - Informe o nome da área (divisão) que deseja excluir.");
                                break;

                            case 7:
                                Console.Clear();

                                Title("MENSAGENS DE ERRO COMUNS");
                                Console.WriteLine();
                                Console.WriteLine("- 'Nome inválido': Nome vazio ou acima do limite.");
                                Console.WriteLine("- 'Residência inexistente': Informe uma residência");
                                Console.WriteLine("  válida.");
                                Console.WriteLine("- 'Andar(Piso) não encontrado': Número do andar(piso)");
                                Console.WriteLine("  não cadastrado.");
                                Console.WriteLine("- 'Área(Divisão) não encontrada': Nome da área");
                                Console.WriteLine("  inexistente.");
                                break;

                            case 8:
                                Console.WriteLine("Obrigado por utilizar o sistema SUPERCLEAN!");
                                again = false;
                                break;

                            default:
                                Console.WriteLine("Opção inválida! Tente novamente.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida! Por favor, insira um número.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro: {ex.Message}");
                }
                if (again)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();

                    }
                }
        }








        }
    }