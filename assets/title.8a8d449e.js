import{_ as m,u as p,L as _,g as f,h as g,r,o as l,j as c,w as h,c as V,z as k,F as v}from"./index.a74f1f35.js";const x={props:["modelValue","disabled"],emits:["update:modelValue"],setup(n,{emit:o}){const a=p(),e=_({formData:n.modelValue}),s=f(()=>a.getters["title/options"]);return g(()=>{a.dispatch("title/getList")}),{state:e,titles:s,change:()=>{o("update:modelValue",e.formData)}}}};function B(n,o,a,e,s,u){const d=r("el-option"),i=r("el-select");return l(),c(i,{modelValue:e.state.formData,"onUpdate:modelValue":o[0]||(o[0]=t=>e.state.formData=t),onChange:e.change,"fit-input-width":!0},{default:h(()=>[(l(!0),V(v,null,k(e.titles,t=>(l(),c(d,{key:t.Key,label:t.Key,value:t.Value},null,8,["label","value"]))),128))]),_:1},8,["modelValue","onChange"])}const D=m(x,[["render",B]]);export{D as default};
