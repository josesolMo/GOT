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

void ConsoleClient::ConsoleComand() {
	string comand;
	string repPath;
	cout << "Console Application" << endl;
	cout << "Por favor Ingrese Comando Disponible" << endl;
	cout << "-----------Comandos disponibles:-----------" << endl;
	cout << "* got init <name>" << endl;
	cout << "* got help" << endl;
	cout << "* got add <-A> / got add <name>" << endl;
	cout << "* got commit <mensaje>" << endl;
	cout << "* got status" << endl;
	cout << "* got rollback" << endl;
	cout << "* got reset" << endl;
	cout << "* got sync" << endl;
	cout << "-------------------------------------------" << endl;

	while (true) {
		cout << ">> ";
		getline(cin, comand);


		if (comand.find("got init") != string::npos) {
			string name = getInLine(comand);
			bool successful = _mkdir(name.c_str());

			if (successful) {
				repPath = name;
				cout << "La carpeta ya existe, ha ingresado a la siguiente carpeta " << name << endl;
				continue;
			}
			
			string json = "{\"NameRepositorie\":\""+name+"\"}";

			std::cout << "Action: Post Archivo" << std::endl;
			auto r = cpr::Post(cpr::Url{ "https://localhost:44348/api/repositorie" },
				cpr::Body{ json },
				cpr::Header{ { "Content-Type", "application/json" } });

			cout << "Se ha creado la carpeta -> " + name + " <- exitosamente" << endl;

			/*
			std::cout << "Obteniendo datos..." << std::endl;
			auto r = cpr::Get(cpr::Url{ "https://localhost:44348/api/repositorie" });
			std::cout << "Respuesta: " << r.text << std::endl;
			*/

			ofstream file;
			file.open(name + "/.gotignore.txt");
			file << "ignorarEjemplo";//se escribe lo que se quiere ignorar
			file.close();


			repPath = name;
			continue;
		}


		if (comand == "got help") {
			cout << "Se recibio el siguiente comando: " << comand << endl;
			continue;
		}



		if (comand.find("got add") != string::npos) {
			string name = getInLine(comand);
			if (!findFiles(repPath, name)) {
				cout << "El archivo ya ha sido agregado anteriormente o no existe, intente de nuevo correctamente" << endl;
				continue;
			}
			continue;
		}

		if (comand.find("got commit") != string::npos) {
			string message = getInLine(comand);
			cout << "Para el directorio " + repPath + " se ha realizado correctamente el commit de los siguientes archivos: " << endl;
			directorioArchivos.display(repPath);
			cout << "Commit: " + message << endl;
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

string ConsoleClient::getInLine(string comand){
	string line;
	int size = comand.size();
	int i = 0;
	int cont = 0;
	while (i < size) {
		if (cont > 0) {
			if (comand.substr(i, 1) == " ") {
				i++;
				while (comand.substr(i, 1) != "") {
					line += comand.substr(i, 1);
					i++;
				}
				break;
			}
			
		}
		else if (comand.substr(i, 1) == " ") {
			cont++;
			
		}
		i++;
	}
	return line;
}

string ConsoleClient::getFileData(string directorio,string name){
	string data;
	FILE* archivo;
	char linea[MAXLIN];
	archivo = fopen((directorio + "/"+ name).c_str(), "rb");
	if (archivo == NULL) {
		cout << "ERROR AL LEER O ENCONTRAR ARCHIVO, VUELVA A INTENTAR DE MANERA CORRECTA" << endl;
		return "";
	}
	int cont = 1;
	while (fgets(linea, MAXLIN, archivo) != NULL) {
		data += linea;
	}
	fclose(archivo);
	return data;
}

void run() {
	ConsoleClient* client = ConsoleClient::getInstance();
	client->ConsoleComand();
}



bool ConsoleClient::findFiles(string directorio,string name){
	DIR* dir;
	
	struct dirent* ent;
	dir = opendir(directorio.c_str());

	if (dir == NULL) {
		cout << ("No se logra abrir el directorio, vuelva a intentar correctamente");
		return false;
	}
	int cont = 0;
	while ((ent = readdir(dir)) != NULL)
	{
		if (name == "-A") {
			if ((strcmp(ent->d_name, directorio.c_str()) != 0) && (strcmp(ent->d_name, "..") != 0) && (strcmp(ent->d_name, ".") != 0)) {
				string names = ent->d_name;
				string data = getFileData(directorio, names);
				if (!directorioArchivos.isInList(directorio, names) && data != "") {
					directorioArchivos.addNode(names, directorio, data);
					cout << "ARCHIVO -> " + names + " <- AGREGADO CORRECTAMENTE,LISTO PARA COMMIT" << endl;
					cont = 1;
				}
			}
		}
		else {
			if ((strcmp(ent->d_name, directorio.c_str()) != 0) && (strcmp(ent->d_name, "..") != 0) && (strcmp(ent->d_name, ".") != 0)) {
				if (ent->d_name == name) {
					string data = getFileData(directorio, name);
					if (!directorioArchivos.isInList(directorio, name) && data != "") {
						directorioArchivos.addNode(name, directorio, data);
						cout << "ARCHIVO -> " + name + " <- AGREGADO CORRECTAMENTE, LISTO PARA COMMIT" << endl;
						break;
						cont = 1;
					}
				}
			}
		}

	}
	closedir(dir);
	if (cont == 0) {
		return false;
	}
	return true;
}

