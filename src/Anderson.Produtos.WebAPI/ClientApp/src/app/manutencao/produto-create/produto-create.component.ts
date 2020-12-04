import { Produto } from '../produto.model'
import { Router } from '@angular/router';
import { ProdutoService } from '../produto.service';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-product-create',
    templateUrl: './produto-create.component.html',
    styleUrls: ['./produto-create.component.css']
})

export class ProdutoCreateComponent implements OnInit {
    produto: Produto = {
        nome: '',
        valor: 0,
        imagemArquivo: null,
    }

    constructor(private produtoService: ProdutoService,
        private router: Router) { }

    ngOnInit(): void {
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

    createProduct(): void {
        if (isNaN(this.produto.valor) || this.produto.valor === 0) {
            console.log(this.produto.valor);
            alert('Valor inválido')
            return;
        }
        this.produtoService.create(this.produto).subscribe(() => {
            alert('Produto registrado com sucesso')
            this.router.navigate(['/'])

        }, (error: HttpErrorResponse) => {
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
}