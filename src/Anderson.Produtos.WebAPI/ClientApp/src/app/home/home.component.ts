import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Produto } from '../manutencao/produto.model';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  Produtos: Array<Produto> = [];

  constructor(http: HttpClient,
    private router: Router,
    private sanitizer: DomSanitizer,
    @Inject('BASE_URL') baseUrl: string) {

    //#temp
    if (!localStorage.getItem('user token')) {
      this.router.navigate(['account/login'])
    }

    http.get<Produto[]>(`${baseUrl}api/Produtos`).subscribe(result => {
      this.Produtos = result;
    }, error => console.error(error));
  }
  sanitizeImageUrl(imageUrl: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(imageUrl);
  }

}
