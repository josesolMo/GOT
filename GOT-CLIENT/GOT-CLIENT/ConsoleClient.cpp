#include "ConsoleClient.h"


void run();

ConsoleClient* ConsoleClient::instance = 0;

ConsoleClient::ConsoleClient() {
	thrd = thread(run);
}

ConsoleClient* ConsoleClient::getInstance() {
	if (instance == 0) {
		instance = new ConsoleClient();
	}
	return instance;
}

void ConsoleClient::ConsoleComand(){
	string comand;
	cout << "Console Application" << endl;
	cout << "Por favor Ingrese Comando Disponible" << endl;
	cout << "-----------Comandos disponibles:-----------" << endl;
	cout << "* got init" << endl;
	cout << "* got help" << endl;
	cout << "* got add[-A][name]" << endl;
	cout << "* got commit" << endl;
	cout << "* got status" << endl;
	cout << "* got rollback" << endl;
	cout << "* got reset" << endl;
	cout << "* got sync" << endl;
	cout << "-------------------------------------------" << endl;
	while (true) {
		cout << ">> ";
		getline(cin,comand);
		if (comand == "got init") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got help") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got add[-A][name]") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got commit") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got status") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got rollback") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got reset") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "got sync") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}
		if (comand == "exit") {
			cout << "TERMINA APLICACION" << endl;
			break;
		}
		else {
			cout << "Comando Invalido" << endl;
			cout << "Por favor Ingrese Comando Disponible" << endl;
		}

	}
}

void run() {
	ConsoleClient* client = ConsoleClient::getInstance();
	client->ConsoleComand();
}