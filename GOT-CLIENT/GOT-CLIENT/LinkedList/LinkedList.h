#pragma once
#include <iostream>
#include "Node.h"
using namespace std;


class LinkedList{
public:
	Node* head;
	Node* tail;
	int size;
	void addNode(string name, string directorio, string data);
	LinkedList();
	void display(string directorio);
	void removeNode(string directorio, string name);
	bool isInList(string directorio,string name);
};

