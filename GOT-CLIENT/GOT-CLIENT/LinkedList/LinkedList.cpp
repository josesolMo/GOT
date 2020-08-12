#include "LinkedList.h"

LinkedList::LinkedList()
{
	head = nullptr;
	tail = nullptr;
	size = 0;
}

void LinkedList::addNode(string name, string directorio, string data){
	Node* temp = new Node;
	temp->data = data;
	temp->dir = directorio + "/" + name;
	temp->directorio = directorio;
	temp->name = name;
	if (head == nullptr) {
		head = temp;
		tail = temp;
	}
	else {
		tail->next = temp;
		tail = tail->next;
	}
	size++;
}

void LinkedList::display(string directorio){
	Node* temp = this->head;
	while (temp != nullptr) {
		if (temp->directorio == directorio) {
			cout << "->" + temp->name<<endl;
		}
		temp = temp->next;
	}
}

void LinkedList::removeNode(string directorio, string name){
	Node* temp = new Node();
	temp = this->head;
	if (temp->directorio == directorio && temp->name == name) {
		temp = temp->next;
		this->head = temp;
	}
	else {
		while (temp->next != nullptr) {
			if (temp->directorio == directorio && temp->name == name) {
				if (temp->next == this->tail) {
					temp->next = temp->next->next;
					this->tail = temp;
					tail->next = temp->next;
					break;
				}
				else {
					temp->next = temp->next->next;
					break;
				}
			}
			temp = temp->next;
		}
	}
	size--;
}

bool LinkedList::isInList(string directorio,string name) {
	Node* temp = this->head;
	while (temp != nullptr) {
		if (temp->directorio == directorio && temp->name == name) {
			return true;
		}
		temp = temp->next;
	}
	delete temp;
	return false;
}