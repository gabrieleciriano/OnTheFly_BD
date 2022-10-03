CREATE DATABASE OnTheFly
USE OnTheFly

CREATE TABLE Iatas
(
	Sigla varchar(3) NOT NULL,
	CONSTRAINT PK_Sigla_Iatas PRIMARY KEY (Sigla),
);


insert into Iatas (Sigla) values ('BSB'),('CGH'),('GIG'),('SSA'),('FLN'),('POA'),('VCP'),('REC'),('CWB'),('BEL'),('VIX'),('SDU'),('CGB'),('CGR'),('FOR'),('MCP'),('MGF'),('GYN'),('NVT'),('MAO'),('NAT'),('BPS'),('MCZ'),('PMW'),('SLZ'),('GRU'),('LDB'),('PVH'),('RBR'),('JOI'),('UDI'),('CXJ'),('IGU'),('THE'),('AJU'),('JPA'),('PNZ'),('CNF'),('BVB'),('CPV'),('STM'),('IOS'),('JDO'),('IMP'),('XAP'),('MAB'),('CZS'),('PPB'),('CFB'),('FEN'),('JTC'),('MOC');
select * from Iatas;

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
	UltimoVoo datetime NOT NULL,
	DataCadastro date NOT NULL,
	Situacao char(1) NOT NULL,

	CONSTRAINT PK_CNPJ_CompanhiaAerea PRIMARY KEY (CNPJ),
);

SELECT * FROM CompanhiaAerea

CREATE TABLE Bloqueados(
	CNPJ varchar(14) NOT NULL,

	CONSTRAINT PK_CNPJ_Bloqueados PRIMARY KEY (CNPJ),
);

CREATE TABLE Aeronave
(
	Inscricao varchar(6) NOT NULL,
	Capacidade int NOT NULL,
	UltimaVenda datetime NOT NULL,
	DataCadastro date NOT NULL,
	Situacao char(1) NOT NULL,
	CompanhiaAerea varchar(14) NOT NULL,
	CONSTRAINT PK_Inscricao_Aeronave PRIMARY KEY(Inscricao),
	CONSTRAINT FK_CompanhiaAerea FOREIGN KEY (CompanhiaAerea) REFERENCES CompanhiaAerea(CNPJ),
);
SELECT * FROM Aeronave

CREATE TABLE Voo
(
	IdVoo varchar(5) NOT NULL,
	IdAeronave varchar(6) NOT NULL,
	DataVoo datetime NOT NULL,
	DataCadastro date NOT NULL,
	Destino varchar(3) NOT NULL,
	AssentosOcupados int NOT NULL,	
	Situacao char(1) NOT NULL,
	CONSTRAINT PK_Voo PRIMARY KEY (IdVoo),
	CONSTRAINT FK_Inscricao_Aeronave FOREIGN KEY (IdAeronave) REFERENCES Aeronave(Inscricao),
	CONSTRAINT PK_Destino FOREIGN KEY(Destino) REFERENCES Iatas(Sigla),
);


select * from Voo

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
SELECT * FROM PASSAGEM

CREATE TABLE PassagemVoo
(
IdVoo varchar(5) NOT NULL,
IdPassagem varchar(6) NOT NULL,

CONSTRAINT FK_IdVoo FOREIGN KEY (IdVoo) REFERENCES Voo(IdVoo),
CONSTRAINT FK_IdPassagem FOREIGN KEY (IdPassagem) REFERENCES Passagem(IdPassagem),

);
SELECT * FROM PassagemVoo

CREATE TABLE Venda
(
	IdVenda int NOT NULL,
	DataVenda date NOT NULL,
	ValorTotal float NOT NULL,
	CpfPassageiro varchar(11) NOT NULL,

	CONSTRAINT PK_IdVenda_Venda PRIMARY KEY (IdVenda),
	CONSTRAINT FK_CPF_Passageiro FOREIGN KEY (CpfPassageiro) REFERENCES Passageiro (CPF)
	
);
SELECT * FROM VENDA

CREATE TABLE ItemVenda
(
	IdItemVenda int identity NOT NULL,
	IdVenda int NOT NULL,
	ValorUnitario float NOT NULL,
	CONSTRAINT PK_IdItemVenda_ItemVenda PRIMARY KEY (IdItemVenda),
	CONSTRAINT FK_IdVenda_Venda FOREIGN KEY (IdVenda) REFERENCES Venda (IdVenda),
);

select * from ItemVenda
