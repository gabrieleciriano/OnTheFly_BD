CREATE DATABASE OnTheFly
USE OnTheFly

CREATE TABLE Iatas
(
	Sigla varchar(3) NOT NULL,
	CONSTRAINT PK_Sigla_Iatas PRIMARY KEY (Sigla),
);

CREATE TABLE Restrito
(
	CPF varchar(11) NOT NULL,
	CONSTRAINT PK_CPF_Restrito PRIMARY KEY(CPF),
);

CREATE TABLE Passageiro
(
	CPF varchar(11) NOT NULL,
	Nome varchar(50) NOT NULL,
	DataNascimento date NOT NULL,
	Sexo char(1) NOT NULL,
	Data_Cadastro date NOT NULL,
	UltimaCompra date NOT NULL,
	Situacao char(1) NOT NULL,

	CONSTRAINT PK_CPF_Passageiro PRIMARY KEY(CPF),
);

SELECT * FROM Passageiro
CREATE TABLE CompanhiaAerea
(
	CNPJ varchar (14) NOT NULL,
	RazaoSocial varchar(50) NOT NULL,
	DataAbertura date NOT NULL,
	UltimoVoo date NOT NULL,
	DataCadastro date NOT NULL,
	Situacao char(1) NOT NULL,

	CONSTRAINT PK_CNPJ_CompanhiaAerea PRIMARY KEY (CNPJ),
);

CREATE TABLE Bloqueados(
	CNPJ varchar(14) NOT NULL,

	CONSTRAINT PK_CNPJ_Bloqueados PRIMARY KEY (CNPJ),
);

CREATE TABLE Aeronave
(
	Inscricao varchar(6) NOT NULL,
	Capacidade int NOT NULL,
	UltimaVenda date NOT NULL,
	DataCadastro date NOT NULL,
	Situacao char(1) NOT NULL,
	CNPJ varchar(14) NOT NULL,
	CONSTRAINT PK_Inscricao_Aeronave PRIMARY KEY(Inscricao),
	CONSTRAINT FK_CompanhiaAerea FOREIGN KEY (CNPJ) REFERENCES CompanhiaAerea(CNPJ),
);

CREATE TABLE Voo
(
	IdVoo varchar(5) NOT NULL,
	Inscricao varchar(6) NOT NULL,
	Situacao char(1) NULL,
	DataVoo date NOT NULL,
	DataCadastro date NOT NULL,
	Destino varchar(3) NOT NULL,
	AssentosOcupados int NOT NULL,	

	CONSTRAINT PK_Voo PRIMARY KEY (IdVoo),
	CONSTRAINT FK_Inscricao_Aeronave FOREIGN KEY (Inscricao) REFERENCES Aeronave(Inscricao),
	CONSTRAINT PK_Destino FOREIGN KEY(Destino) REFERENCES Iatas(Sigla),
);

CREATE TABLE Passagem
(
	IdPassagem varchar(6) NOT NULL,
	IdVoo varchar(5) NOT NULL,
	DataUltimaOperacao date NOT NULL,
	Valor float NOT NULL,
	Situacao char(1) NOT NULL,
	
	
	CONSTRAINT PK_IdPassagem_Passagem PRIMARY KEY(IdPassagem),
	CONSTRAINT FK_IdVoo_Voo FOREIGN KEY (IdVoo) REFERENCES Voo (IdVoo),

);

CREATE TABLE Venda
(
	IdVenda varchar(5) NOT NULL,
	DataVenda date NOT NULL,
	ValorTotal float NOT NULL,
	CPF varchar(11) NOT NULL,

	CONSTRAINT PK_IdVenda_Venda PRIMARY KEY (IdVenda),
	CONSTRAINT FK_CPF_Passageiro FOREIGN KEY (CPF) REFERENCES Passageiro (CPF)
	
);

CREATE TABLE ItemVenda
(
	IdItemVenda varchar(5) NOT NULL,
	IdVenda varchar(5) NOT NULL,
	ValorUnitario float NOT NULL,
	CONSTRAINT PK_IdItemVenda_ItemVenda PRIMARY KEY (IdItemVenda),
	CONSTRAINT FK_IdVenda_Venda FOREIGN KEY (IdVenda) REFERENCES Venda (IdVenda),
);



