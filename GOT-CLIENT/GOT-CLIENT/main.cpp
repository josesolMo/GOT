
#include<iostream>
#include<cpr/cpr.h>
#include "Client/ConsoleClient.h"

using namespace std;

int main() {
	
	ConsoleClient* client = ConsoleClient::getInstance();
	client->thrd.join();
	return 0;
	
	/*
	{
		std::cout << "Obteniendo datos..." << std::endl;
		auto r = cpr::Get(cpr::Url{ "https://localhost:44348/api/archivo" });
		std::cout << "Respuesta: " << r.text << std::endl;
	}
	*/
	/*
	{
		std::cout << "Action: Post Archivo" << std::endl;
		auto r = cpr::Post(cpr::Url{ "http://localhost:44348/api/archivo" },
			cpr::Body{ R"({"IDArchivo":"<id>", 
            "DireccionArchivo":"<dir>","DataArchivo":"<data>","NombreArchivo":"<name>"})" },
			cpr::Header{ { "Content-Type", "application/json" } });
		std::cout << "Returned Status:" << r.status_code << std::endl;
	}
	*/
	/*
	FILE* archivo;
	string mensaje = "";
	char linea[MAXLIN];
	archivo = fopen("C:/Users/mario/Desktop/free.txt", "rb");
	if (archivo == NULL) {
		cout << "ERROR" << endl;
		return 0;
	}
	int cont = 1;
	while (fgets(linea, MAXLIN, archivo) != NULL) {
		mensaje += linea;
	}
	fclose(archivo);
	cout << mensaje << endl;
	return 1;
	*/
}