/**
* @file ConsoleClient.h
* @version 1.0
* @date 4/08/2020
* @author Mario Gudino Rovira
* @title ConsoleClient
* @brief Cliente de la aplicacion
*/
#pragma once
#include <iostream>
#include <thread>
#include <string>

using namespace std;

class ConsoleClient{
public:
	thread thrd;

	/**
	* @brief getInstance obtiene o crea singleton cliente
	* @return referencia instanciada de la clase
	*/
	static ConsoleClient* getInstance();

	/**
	* @brief ConsoleComando metodo que ejecuta comandos en consola
	*/
	void ConsoleComand();
private:
	/**
	* @brief ConsoleClient constructor de la clase
	*/
	ConsoleClient();
	static ConsoleClient* instance;
};

