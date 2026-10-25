# 10 — Add doctor

Commit: `Add doctor implemented`. PS: `ps/15` and the add-doctor HTML notes.

## Component

```typescript
addDoctor(doctorName: string) {
  this._curewellService.addDoctor(doctorName).subscribe({
    next: (response) => {
      this.status = response;
      this.showDiv = true;
      this.msg = 'Doctor successfully added';
    },
    error: (error) => {
      this.errorAddMsg = error;
      this.showDiv = true;
      this.msg = 'Some error occured';
    },
    complete: () => {
      console.log('Add doctor completed');
    }
  });
}
```

| PS | Code |
|---|---|
| call service | `addDoctor(doctorName)` |
| `status` | `this.status = response` |
| `showDiv` | `true` so the HTML `*ngIf="showDiv"` prints `msg` |
| success `msg` | `'Doctor successfully added'` |
| error → `errorAddMsg` and `msg` | `'Some error occured'` (spelling in the table) |
| console | `'Add doctor completed'` in `complete` |

Form: `(ngSubmit)="addDoctor(doctorName)"`. Validation text is [05](05-template-forms.md).

## Gap vs a strict reading

`next` sets the success message whenever HTTP returns, including body `false`. The table says “on successful addition”. If `response` is `false`, set `msg` to the failure text and leave the success text for `response === true`.
