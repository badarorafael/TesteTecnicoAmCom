using System.Globalization;
using System.Xml.Linq;

namespace Questao1
{
    class ContaBancaria
    {
        public int Conta { get { return _conta; } }
        public string Titular { get { return _titular; } }
        public double Saldo { get { return _saldo; } }

        private int _conta { get; set; }
        private string _titular { get; set; }
        private double _saldo { get; set; }


        private double TaxaInstituicao = 3.5;

        public ContaBancaria(int numero, string titular)
        {
            _conta = numero;
            _titular = titular;
            _saldo = 0;
        }
        public ContaBancaria(int numero, string titular, double depositoInicial = 0)
        {
            _conta = numero;
            _titular = titular;
            _saldo = depositoInicial;
        }
        public void AlteraNomeTitular(string titular)
        {
            _titular = titular;
        }
        public void Deposito(double quantia = 0)
        {
            _saldo += quantia;
        }

        public void Saque(double quantia = 0)
        {
            _saldo -= (TaxaInstituicao + quantia);
        }

        public override string ToString()
        {
            return $"Conta {_conta}, Titular: {_titular}, Saldo: {_saldo.ToString("C")}";
        }
    }
}
