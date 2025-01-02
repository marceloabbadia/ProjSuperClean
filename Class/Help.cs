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
            Console.WriteLine("1 - Atalhos Super Rápidos: Incluir e Excluir Limpeza");
            Console.WriteLine("2 - Gerenciar Limpeza: Consulta das Limpezas das Áreas");
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
                                Utils.Utils.PrintSucessMessage("Atalho rápido: Opção 1 - Registrar limpeza.");
                                Console.WriteLine("Nos menus, você pode registrar a limpeza da área.");
                                Console.WriteLine("As áreas serão listadas por vencimento, começando pelas");
                                Console.WriteLine("mais próximas ou já vencidas. Ao informar o código da área,");
                                Console.WriteLine("a limpeza será registrada automaticamente com a data atual, e");
                                Console.WriteLine("a próxima será recalculada conforme o intervalo cadastrado.");
                                Console.WriteLine();

                                Console.WriteLine();
                                Title("REGISTRAR EXCLUSÃO DA LIMPEZA DA ÁREA");
                                Console.WriteLine();
                                Utils.Utils.PrintSucessMessage("Atalho rápido: Opção 2 - Excluir última limpeza.");
                                Console.WriteLine("Você também pode excluir a última limpeza registrada,");
                                Console.WriteLine("restaurando a data anterior conforme o intervalo cadastrado.");
                                Console.WriteLine();
                                Console.WriteLine();


                                break;

                            case 2:
                                Console.Clear();

                                Title("AJUDA - GERENCIAR LIMPEZAS");
                                Console.WriteLine();
                                Console.WriteLine("Este módulo permite ao utilizador gerenciar as limpezas das áreas cadastradas.");
                                Console.WriteLine();
                                Console.WriteLine("1 - Consultar Limpezas");
                                Console.WriteLine("   - Visualiza as limpezas realizadas e as próximas programadas.");
                                Console.WriteLine( );
                                Console.WriteLine("2 - Simulador de Datas");
                                Console.WriteLine("   - Simula datas de limpeza progressivas ou regressivas e visualiza");
                                Console.WriteLine("     as áreas com barras coloridas indicando o estado de limpeza.");
                                Console.WriteLine();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("||| BARRAS VERDES");
                                Console.ResetColor();
                                Console.WriteLine("- Divisão limpa.");
                                Console.WriteLine("- Barras exibidas até o limite de 20 dias.");

                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("||| BARRAS AMARELAS");
                                Console.ResetColor();
                                Console.WriteLine("- Divisão entre 1 dia antes e 2 dias após a limpeza.");
                                Console.WriteLine( );
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("||| BARRAS VERMELHAS");
                                Console.ResetColor();
                                Console.WriteLine("- Divisão com limpeza vencida há mais de 2 dias.");

                                Console.WriteLine("- Barras exibidas até o limite de 20 dias.");
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