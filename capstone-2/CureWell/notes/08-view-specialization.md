# 08 — View specialization

Commit: `specializations made`. PS: `ps/13`.

## ngOnInit / getSpecialization

```typescript
ngOnInit() {
  this.getSpecialization();
}

getSpecialization() {
  this._curewellService.getAllSpecializations().subscribe({
    next: (res) => {
      this.specializationList = res;
    },
    error: (error) => {
      this.specializationList = null;
      this.errorMsg = error;
    }
  });
}
```

| PS | Committed code |
|---|---|
| service method | `getAllSpecializations()` |
| success → `specializationList` | `next` |
| error → list `null`, `errorMsg` | `error` |

The table also says: set `showMsgDiv`, and `console.log('Specialization Fetched Successfully')`. Those two lines are not in this commit. Add them in `next` / `complete` if the evaluator checks the console and the “No Specialization Available” block (`*ngIf="showMsgDiv"` in the HTML).

## HTML

```html
<tr *ngFor="let singlespec of specializationList">
  <td>{{singlespec.specializationCode}}</td>
  <td>{{singlespec.specializationName}}</td>
</tr>
```

Property names match `specialization.ts`, which match the API’s camelCase JSON.
