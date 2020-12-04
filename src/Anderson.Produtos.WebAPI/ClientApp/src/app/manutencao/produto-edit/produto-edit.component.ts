import { Produto } from '../produto.model'
import { ActivatedRoute, Router } from '@angular/router';
import { ProdutoService } from '../produto.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'app-product-edit',
    templateUrl: './produto-edit.component.html',
    styleUrls: ['./produto-edit.component.css']
})

export class ProdutoEditComponent implements OnInit {
    produto: Produto = {
        nome: 'teste',
        valor: 10,
        imagemArquivo: null,
        imagemPath: null,
    }

    constructor(private produtoService: ProdutoService,
        private activatedRoute: ActivatedRoute,
        private router: Router) {

    }
    valorChange(event: any): void {
        this.produto.valor = Number(event.target.value.replace(',', '.'));
    }
    nomeChange(event: any): void {
        this.produto.nome = event.target.value;
    }
    fileChangeEvent(event): void {
        this.produto.imagemArquivo = event.target.files[0];
    }

    ngOnInit(): void {
        this.produto.id = Number(this.activatedRoute.snapshot.paramMap.get("id"));

        this.produtoService.readById(this.produto.id).subscribe((produto) => {
            this.produto.id = produto.id;
            this.produto.nome = produto.nome;
            this.produto.valor = produto.valor;
            this.produto.imagemPath = produto.imagemPath;

            console.log('this.produto lido', this.produto)
        })
    }

    save() {
        if (isNaN(this.produto.valor) || this.produto.valor === 0) {
            console.log(this.produto.valor);
            alert('Valor inválido')
            return;
        }

        console.log('this.produto', this.produto)
        this.produtoService.update(this.produto).subscribe(() => {
            alert('Produto alterado com sucesso')
            this.router.navigate(['/'])

        }, (error: HttpErrorResponse) => {
            console.log(error.error)
            let serverError = error.error.Error;
            if (serverError)
                alert(serverError)
            else
                alert("Algo inesperado aconteceu")

            console.log(error.error)
        })
    }

    cancel(): void {
        this.router.navigate(['/'])
    }
    remove(): void {
        console.log('apagando o id', this.produto.id)
        this.produtoService.delete(this.produto.id).subscribe(() => {
            alert('Produto removido com sucesso')
            this.router.navigate(['/'])
            return;

        }, (error: HttpErrorResponse) => {
            let serverError = error.error.Error;
            if (serverError)
                alert(serverError)
            else
                alert("Algo inesperado aconteceu")

            console.log(error.error)
        })
    }
}