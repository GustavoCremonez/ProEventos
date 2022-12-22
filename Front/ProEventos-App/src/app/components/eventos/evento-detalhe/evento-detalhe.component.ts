import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent {
  form: FormGroup;

  get formControls(): any {
    return this.form.controls;
  }

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.formBuilder.group({
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(5),
          Validators.maxLength(50),
        ],
      ],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }
}
