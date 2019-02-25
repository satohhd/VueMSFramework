<template>
    <div class="d-inline">
        <template v-if="field.type === '1'">
            <h1 :id="_id" :class="field.name"><span v-html="field.caption"></span></h1>
        </template>
        <template v-else-if="field.type === '2'">
            <h2 :id="_id" :class="field.name"><span v-html="field.caption"></span></h2>
        </template>
        <template v-else-if="field.type === '3'">
            <h3 :id="_id" :class="field.name"><span v-html="field.caption"></span></h3>
        </template>
    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {


        },
 
    }

</script>
<style scoped>
</style>
